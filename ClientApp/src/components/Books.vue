<template>
    <h1 id="tableLabel">Books catalogue</h1>
    <p v-if="message">{{message}}</p>
    <p v-if="!books"><em>Loading...</em></p>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="books">
        <thead>
            <tr>
                <th>Title</th>
                <th>Author</th>
                <th>Year Published</th>
                <th>Status</th>
                <th>Description</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="book of books" v-bind:key="book">
                <td>{{ book.title }}</td>
                <td>{{ book.author }}</td>
                <td>{{ book.yearPublished }}</td>
                <td>{{ bookStatuses[book.status]}} </td>
                <td width="40%">{{ book.description}}</td>
                <!--<td><button v-if="isLibrarian" @click=borrow(book)>Borrow</button></td>-->
                <td>
                    <button v-if="isLibrarian && bookStatuses[book.status]=='Taken'" @click=borrow(book) class="btn btn-info">Get in queue</button>
                    <button v-if="isLibrarian && bookStatuses[book.status]=='Free'" @click=borrow(book) class="btn btn-info">Request the book</button>
                    <button disabled v-if="isLibrarian && bookStatuses[book.status]=='Already requested'" @click=borrow(book) class="btn btn-info">Already requested</button>
                    <!--<td><button v-if="isLibrarian" @click=getInQueue(book)>Get in queue for the book</button></td>-->
                    <button v-if="isLibrarian" @click=deleteBook(book) class="btn btn-warning">Delete</button>
                </td>
            </tr>
        </tbody>
    </table>
    <add-book v-if="isLibrarian" @newbook="addNewBook"></add-book>

</template>


<script>
    import AddBook  from '@/components/AddBook.vue'
    import createHttp from "@/services/http";
    export default {
        name: "Books",
        data() {
            return {
                books: [],
                message: "",
                http: "",
                bookStatuses:
                {
                    1: "Free",
                    2: "Taken",
                    3: "Already requested"
                }
            }
        },
        computed: {            
            isLibrarian: function () { return this.$store.getters.isLibrarian },
        },
        components: {
            AddBook
        },
        methods: {
            getBooks() {
                this.http.get('/api/books')
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
                this.http.delete('/api/books/' + book.id)
                    .then(() => {
                        this.message = "Book removed";
                        this.books.splice(this.books.indexOf(book), 1);                       
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            borrow(book) {
                console.log(book);
                this.http.put('/api/books/' + book.id +'/order')
                    .then((response) => {
                        this.message = response.data.message;
                        book.status = 3;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.http = createHttp(true);
            this.getBooks();
        }
    }
</script>