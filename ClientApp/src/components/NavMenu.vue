<template>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">
                <a v-if="isAuthenticated" class="navbar-brand">Dear {{name}}, Welcome to LibraryApp!</a>
                <a v-if="!isAuthenticated" class="navbar-brand">Welcome to LibraryApp!</a>
                <button class="navbar-toggler"
                        type="button"
                        data-toggle="collapse"
                        data-target=".navbar-collapse"
                        aria-label="Toggle navigation"
                        @click="toggle">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse"
                     v-bind:class="{show: isExpanded}">
                    <div v-if="isAuthenticated">
                        <ul class="navbar-nav flex-grow">
                            <li class="nav-item">
                                <router-link :to="{ name: 'Books' }" class="nav-link text-dark">List of books</router-link>
                            </li>
                        </ul>
                        <ul v-if="isLibrarian" class="navbar-nav flex-grow">
                            <li class="nav-item">
                                <router-link :to="{ name: 'Users' }" class="nav-link text-dark">List of users</router-link>
                            </li>
                        </ul>
                        <ul class="navbar-nav flex-grow">
                            <li class="nav-item">
                                <span class="nav-link text-dark" @click="logout">Logout</span>
                            </li>
                        </ul>
                    </div>
                    <div v-if="!isAuthenticated">
                        <ul class="navbar-nav flex-grow">
                            <li class="nav-item">
                                <router-link :to="{ name: 'Login' }" class="nav-link text-dark">Login</router-link>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </nav>
    </header>
</template>


<style>
    a.navbar-brand {
        white-space: normal;
        text-align: center;
        word-break: break-all;
    }

    html {
        font-size: 14px;
    }

    @media (min-width: 768px) {
        html {
            font-size: 16px;
        }
    }
    .box-shadow {
        box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
    }
</style>
<script>

    export default {
        name: "NavMenu",
        data() {
            return {
                isExpanded: false                
            }
        },
        computed: {
            isAuthenticated: function () { return this.$store.getters.isAuthenticated },
            name: function () { return this.$store.getters.userDetails },
            isLibrarian: function () { return this.$store.getters.isLibrarian },
        },
        methods: {
            collapse() {
                this.isExpanded = false;
            },

            toggle() {
                this.isExpanded = !this.isExpanded;
            },
            logout: function () {
                this.$store.dispatch("logout").then(() => {
                    this.$router.push("/login");
                })
            }
        }
    }
</script>