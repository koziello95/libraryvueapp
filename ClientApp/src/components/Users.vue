<template>
    <h1 id="tableLabel">List of Readers</h1>
    <p v-if="message">{{message}}</p>
    <p v-if="!users"><em>Loading...</em></p>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="users">
        <thead>
            <tr>
                <th>Firstname</th>
                <th>Lastname</th>
                <th>Login</th>
                <th>Role</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="user of users" v-bind:key="user">
                <td>{{ user.firstname }}</td>
                <td>{{ user.lastname}}</td>
                <td>{{ user.login}}</td>
                <td>{{ user.roles}}</td>
              
                <td>
                    <button v-if="isLibrarian && !user.roles.includes('Librarian')" class="btn btn-info" @click=assignLibrarian(user)>Assign Librarian role</button>
                    <button class="btn btn-warning" @click=deleteUser(user)>Delete account</button>
                </td>
            </tr>
        </tbody>
    </table>

</template>


<script>
    import createHttp from "@/services/http";

    export default {
        name: "Users",
        data() {
            return {
                users: [],
                message: "",
                http: ""
            }
        },
        computed: {
            isLibrarian: function () { return this.$store.getters.isLibrarian }
        },
        methods: {
            getUsers() {
                this.http.get('/api/users')
                    .then((response) => {
                        this.users =  response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            deleteUser(user) {
                this.http.delete('/api/users/' + user.id)
                    .then(() => {
                        this.message = "User deleted";
                        this.users.splice(this.users.indexOf(user), 1);
                    })
                    .catch(function (error) {
                        alert(error);
                    });               
            },
            assignLibrarian(user) {
                this.http.post('/api/users/' + user.id + '/assignlibrarian')
                    .then(() => {
                        this.getUsers();
                    })
                    .catch(function (error) {
                        alert(error);
                    });     
            }
        },
        mounted() {
            this.http = createHttp(true);
            this.getUsers();
        }
    }
</script>