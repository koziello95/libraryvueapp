<template> 
        <input v-model="model.title" type="text" placeholder="Book Title" />
        <input v-model="model.yearPublished" type="number" placeholder="2000" />
        <input v-model="model.author" type="text" placeholder="Author" />
        <input v-model="model.description"  type="text" placeholder="Short description" />
        <button @click="save">Add book</button>
  
    <p>{{message}}</p>
</template>

<script>
       import axios from 'axios'

    export default {
        name: "AddBook",        
        data() {
            return {
                model: {
                    id: 0,
                    title: "",
                    yearPublished: 0,
                    author: "",
                    description: ""
                },
                message:""
            }
        },
        methods: {
            save() {
                axios.post('/api/books', this.model)
                    .then((response) => {
                        this.message = response.statusText;      
                        this.$emit('newbook', response.data);
                        return false;
                    })
                    .catch(function (error) {
                        alert(error);
                        return false;
                    });
            }
        }

    };
</script>