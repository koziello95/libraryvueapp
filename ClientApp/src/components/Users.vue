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
            </tr>
        </thead>
        <tbody>
            <tr v-for="user of users" v-bind:key="user">
                <td>{{ user.firstname }}</td>
                <td>{{ user.lastname}}</td>
                <td>{{ user.login}}</td>
                <td>{{ user.role}}</td>
                <td><button @click=deleteBook(book)>Delete account</button></td>
            </tr>
        </tbody>
    </table>
    <!--<add-book @newuser="addNewUser"></add-book>-->

</template>


<script>
    //import AddBook  from '@/components/AddBook.vue'
    import axios from 'axios'

    export default {
        name: "Users",
        data() {
            return {
                users: [],
                message:""
            }
        },
        //components: {
        //    AddBook
        //},
        methods: {
            getUsers() {
                axios.get('/api/users')
                    .then((response) => {
                        this.users =  response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            //addNewBook(book) {
            //    this.books.push(book);
            //},
            deleteBook(user) {
                axios.delete('/api/users/' + user.id)
                    .then(() => {
                        this.message = "Book removed";
                        this.books.splice(this.user.indexOf(user), 1);
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getUsers();
        }
    }
</script>