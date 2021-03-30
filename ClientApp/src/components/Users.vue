<template>
    <h1 id="tableLabel">List of users</h1>
    <p v-if="message">{{message}}</p>
    <p v-if="!users"><em>Loading...</em></p>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="users">
        <thead>
            <tr>
                <th>Firstname</th>
                <th>Lastname</th>
                <th>Year Published)</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="book of books" v-bind:key="book">
                <td>{{ book.title }}</td>
                <td>{{ book.author }}</td>
                <td>{{ book.yearPublished }}</td>
                <td width="40%">{{ book.description}}</td>
                <td><button @click=deleteBook(book)>Delete</button></td>
            </tr>
        </tbody>
    </table>
    <add-book @newbook="addNewBook"></add-book>

</template>


<script>
    import AddBook  from '@/components/AddBook.vue'
    import axios from 'axios'

    export default {
        name: "Books",
        data() {
            return {
                books: [],
                message:""
            }
        },
        components: {
            AddBook
        },
        methods: {
            getBooks() {
                console.log(this.books)
                axios.get('/api/books')
                    .then((response) => {
                        this.books =  response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            addNewBook(book) {
                this.books.push(book);
            },
            deleteBook(book) {
                console.log(this.books);
                axios.delete('/api/books/' + book.id)
                    .then(() => {
                        this.message = "Book removed";
                        this.books.splice(this.books.indexOf(book), 1);
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getBooks();
        }
    }
</script>