<template>
    <div>
        <h1>Register</h1>
        <form novalidate @submit.prevent="onSubmit()">
            <div class="form-group">
                <label for="username">Firstname</label>
                <input type="text" name="firstname" v-model="model.firstname" class="form-control" />
            </div>
            <div class="form-group">
                <label for="username">Lastname</label>
                <input type="text" name="lastname" v-model="model.lastname" class="form-control" />
            </div>
            <div class="form-group">
                <label for="username">Login</label>
                <input type="text" name="login" v-model="model.login" class="form-control" />
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" name="password" v-model="model.password" class="form-control" />
            </div>
            <div class="form-group">
                <input type="submit" class="btn btn-success" value="Create account" />&nbsp;

            </div>
        </form>
        <div>
            <router-link class="btn btn-info" to="/login">Proceed to login</router-link>
        </div>
    </div>

</template>

<script>
    import { reactive } from "vue";
    import axios from "axios";
    export default {
        name: "Register",  
        setup() {

            const model = reactive({ login: "", password: "" , lastname:"", firstname:""});
            function onSubmit() {
                axios.post("api/auth/register", model)
                    .then(() => { 
                        alert("Account created successfully");
                    })
                    .catch(error => {
                        if (error.response.data.errors != undefined)
                            alert(JSON.stringify(error.response.data.errors));
                        else
                            alert(JSON.stringify(error.response.data));
                    })
            }
            return {
                model,
                onSubmit
            }
        }
    }
</script>

<style>
    .form-control {
        width: 60%;      
        margin: 0 auto;
    }
</style>