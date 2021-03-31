<template>
    <table class='table table-striped' aria-labelledby="tableLabel" v-if="queueItems">
        <thead>
            <tr>
                <th>Login</th>
                <th>Full name</th>
                <th>Status</th>
                <th>Expected return date</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="queueItem of queueItems" v-bind:key="queueItem">
                <td>{{ queueItem.login }}</td>
                <td>{{ queueItem.firstname }} {{ queueItem.lastname }}</td>
                <td>{{ bookOrderStatuses[queueItem.status] }}</td>
                <td>{{ queueItem.expectedReturnDate }}</td>
                <td>
                    <button v-if="!queueItems.some((item)=> item.status == 2) && queueItem.status != 3" @click=dispose(queueItem) class="btn btn-info">Dispose book to reader</button>
                    <button v-if="queueItem.status == 2" @click=returnBook(queueItem) class="btn btn-info">Return book</button>
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    import createHttp from "@/services/http";

    export default {
        name: "BookQueue",
        props: {
            book: {}
        },
        data() {
            return {
                queueItems: [],
                message: "",
                http: "",
                    bookOrderStatuses:
                {
                    1: "Requested",
                    2: "Borrowed",
                    3: "Returned"
                }
            }
        },
        methods: {
            getQueue() {
                this.http.get('/api/books/' + this.$route.params.id + '/queue')
                    .then((response) => {
                        this.queueItems = response.data;
                        return false;
                    })
                    .catch(function (error) {
                        alert(error);
                        return false;
                    });
            },
            dispose(queueItem) {
                this.http.put('/api/books/'+ this.$route.params.id + "/dispose/"+ queueItem.bookOrderId)
                    .then((response) => {
                        alert(response.data.message);
                        queueItem.status = 2;//borrowed
                    })
                    .catch((error) => {
                        console.log(error);
                         alert(error.response.data);
                    });
            },
            returnBook(queueItem) {
                this.http.put('/api/books/' + this.$route.params.id + "/return", queueItem.userId, {headers: {"Content-Type": "application/json"}})
                    .then((response) => {
                        queueItem.status = 3;
                        alert(response.data.message);
                        return false;
                    })
                    .catch((error) => {
                        alert(error.response.data);
                        return false;
                    });
            }
        },
        mounted() {
            this.http = createHttp(true);     
            this.getQueue();
        }

    };
</script>