import { createWebHistory, createRouter } from "vue-router";
import Home from "@/components/Home.vue";
import Counter from "@/components/Counter.vue";
import FetchData from "@/components/FetchData.vue";
import Login from '@/components/Login.vue'
import Register from '@/components/Register.vue'
import store from "@/store";


const authGuard = (to, from, next) => {
    if (store.getters.isAuthenticated) {
        next();
    } else {
        next("/login")
    }
};


const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
        beforeEnter: authGuard
    },
    {
        path: "/Counter",
        name: "Counter",
        component: Counter,
        beforeEnter: authGuard
    },
    {
        path: "/FetchData",
        name: "FetchData",
        component: FetchData,
        beforeEnter: authGuard
    },
    {
        path: '/register',
        name: 'Register',
        component: Register
    },
    {
        path: '/login',
        name: 'Login',
        component: Login
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});


export default router;