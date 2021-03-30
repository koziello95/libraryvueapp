import { createWebHistory, createRouter } from "vue-router";
import Books from "@/components/Books.vue";
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
        path: "/books",
        name: "Books",
        component: Books,
        beforeEnter: authGuard
    },
    {
        path: "/",
        name: "Books",
        component: Books,
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