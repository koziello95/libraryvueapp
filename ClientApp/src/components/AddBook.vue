<template>
    <h3>Add a new book in below form</h3>
    <div class="form-group">
        <input v-model="model.title" type="text" placeholder="Book Title" />
    </div>
    <div class="form-group">
        <input v-model="model.yearPublished" type="number" placeholder="2000" />
    </div>
    <div class="form-group">
        <input v-model="model.author" type="text" placeholder="Author" />
    </div>
    <div class="form-group">
        <input v-model="model.description" type="text" placeholder="Short description" />
    </div>
    <div class="form-group">
        <button class="btn btn-success" @click="save">Add book</button>
    </div>
    <p>{{message}}</p>
</template>

<script>
    import createHttp from "@/services/http";

    export default {
        name: "AddBook",
        data() {
            return {
                model: {
                    id: 0,
                    title: "",
                    yearPublished: 0,
                    author: "",
                    description: "",
                    status: 1 //free
                },
                message: "",
                http: "",
            }
        },
        methods: {
            save() {
                this.http.post('/api/books', this.model)
                    .then((response) => {
                        this.message = response.statusText;
                        response.data.status = 1;//free
                        this.$emit('newbook', response.data);
                        return false;
                    })
                    .catch(function (error) {
                        alert(error);
                        return false;
                    });
            }
        },
        mounted() {
            this.http = createHttp(true);
        }

    };
</script>