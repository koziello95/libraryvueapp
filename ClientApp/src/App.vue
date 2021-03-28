<template>
    <nav-menu></nav-menu>
    <router-view />



    <div>
        <header>
            <b-navbar toggleable="md" type="light" variant="light">
                <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
                <b-navbar-brand to="/">Love Pizza</b-navbar-brand>
                <b-collapse is-nav id="nav-collapse">
                    <b-navbar-nav>
                        <b-nav-item href="#" @click.prevent="login" v-if="!user">Login</b-nav-item>
                        <b-nav-item href="#" @click.prevent="logout" v-else>Logout</b-nav-item>
                    </b-navbar-nav>
                </b-collapse>
            </b-navbar>
        </header>
        <main>
            <div>
                Love pizza button and clicks counter here
            </div>
        </main>
    </div>
</template>

<script>
    import NavMenu from './components/NavMenu.vue'

    export default {
        name: 'App',
        components: {
            NavMenu
        },
        data() {
            return {
                user: null
            }
        },
        async created() {
            await this.refreshUser()
        },
        watch: {
            '$route': 'onRouteChange'
        },
        methods: {
            login() {
                this.$auth.loginRedirect()
            },
            async onRouteChange() {
                await this.refreshUser()
            },
            async refreshUser() {
                this.user = await this.$auth.getUser()
            },
            async logout() {
                await this.$auth.logout()
                await this.refreshUser()
                this.$router.push('/')
            }
        }
    }
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}


#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
