<template>
    <h1 id="tableLabel">Books catalogue</h1>
    <p v-if="!books"><em>Loading...</em></p>

    <div class="input-group mb-3">
        <input class="form-control"
               type="text"
               placeholder="Search anything"
               v-model="filter" />
    </div>
    <table class='table table-striped' aria-labelledby="tableLabel" v-if="filteredBooks">
        <thead>
            <tr>
                <th>Title</th>
                <th>Author</th>
                <th>Year Published</th>
                <th>Description</th>
                <th>Status</th>
                <th v-if="isLibrarian">Expected return date</th>
                <th>Queue length</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="book of filteredBooks" v-bind:key="book">
                <td>{{ book.title }}</td>
                <td>{{ book.author }}</td>
                <td>{{ book.yearPublished }}</td>
                <td width="20%">{{ book.description}}</td>
                <td>{{ bookStatuses[book.status]}} </td>
                <th width="10%" v-if="isLibrarian">{{book.expectedReturnDate}}</th>
                <td>{{ book.queueLength}} </td>
                <!--<td><button v-if="isLibrarian" @click=borrow(book)>Borrow</button></td>-->
                <td class="btn-group">
                    <button v-if="isReader && bookStatuses[book.status]=='Taken'" @click=borrow(book) class="btn btn-info">Get in queue</button>
                    <button v-if="isReader && bookStatuses[book.status]=='Free'" @click=borrow(book) class="btn btn-info">Request the book</button>
                    <button disabled v-if="isReader && bookStatuses[book.status]=='Already requested'" @click=borrow(book) class="btn btn-info">Already requested</button>
                    <button v-if="isReader && bookStatuses[book.status]=='Taken' && book.isAbleToReturn" @click=returnBook(book) class="btn btn-info">Return book</button>
                    <!--<td><button v-if="isLibrarian" @click=getInQueue(book)>Get in queue for the book</button></td>-->
                    <button v-if="isLibrarian" @click=showQueue(book) class="btn btn-secondary">Show readers queue</button>
                    <button v-if="isLibrarian" @click=deleteBook(book) class="btn btn-warning">Delete</button>
                </td>
            </tr>
        </tbody>
    </table>
    <add-book v-if="isLibrarian" @newbook="addNewBook"></add-book>

</template>


<script>
    import AddBook from '@/components/AddBook.vue'
    import createHttp from "@/services/http";
    import router from "@/router";


    export default {
        name: "Books",
        data() {
            return {
                books: [],
                allBooks:[],
                http: "",
                bookStatuses:
                {
                    1: "Free",
                    2: "Taken",
                    3: "Already requested"
                },
                hasOverdueBooks: false,
                filter:''
            }
        },
        computed: {
            isLibrarian: function () { return this.$store.getters.isLibrarian },
            isReader: function () { return this.$store.getters.isReader },
            filteredBooks() {
                return this.books.filter(book => {
                    const titles = book.title.toString().toLowerCase();
                    const authors = book.author.toLowerCase();
                    const descriptions = book.description.toLowerCase();
                    const statuses = this.bookStatuses[book.status].toLowerCase();
                    const searchTerm = this.filter.toLowerCase();

                    return titles.includes(searchTerm) ||
                        authors.includes(searchTerm) ||
                        descriptions.includes(searchTerm) ||
                        statuses.includes(searchTerm);

                })
            }
        },
        components: {
            AddBook
        },
        methods: {
            getBooks() {
                this.http.get('/api/books')
                    .then((response) => {
                        this.allBooks = this.books = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            checkIfHasOverdueBooks() {
                this.http.get('/api/users/' + this.$store.state.userId +'/hasoverduebooks')
                    .then((response) => {
                        if (response.data.success === false) {
                            this.hasOverdueBooks = true;
                            alert(response.data.message);
                        }
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
                        alert("Book removed successfully.")
                        this.books.splice(this.books.indexOf(book), 1);                       
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            borrow(book) {
                this.http.put('/api/books/' + book.id +'/order')
                    .then((response) => {
                        alert(response.data.message);
                        book.status = 3;
                        book.queueLength++;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            showQueue(book) {
                router.push("/books/" + book.id + "/queue");
            },
            returnBook(book) {
                console.log(this.$store.state.userId);
                this.http.put('/api/books/' + book.id + "/return", this.$store.state.userId, {headers: {"Content-Type": "application/json"}})
                    .then((response) => {
                        alert(response.data.message);
                        book.status = 1;//free
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
            this.getBooks();
            this.checkIfHasOverdueBooks();
        }
    }
</script>


<style>
    .btn{
        margin-left: 2px;
        margin-right: 2px;
    }
</style>