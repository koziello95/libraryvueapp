import { createWebHistory, createRouter } from "vue-router";
import Books from "@/components/Books.vue";
import Users from "@/components/Users.vue";
import Login from '@/components/Login.vue'
import BookQueue from '@/components/BookQueue.vue'
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
        path: "/books/:id/queue",
        name: "BookQueue",
        component: BookQueue,
        beforeEnter: authGuard,
        props: true
    },
    {
        path: "/users",
        name: "Users",
        component: Users,
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

router.afterEach(() => {
    store.commit("clearError");
});

export default router;