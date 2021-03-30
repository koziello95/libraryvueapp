import createHttp from "@/services/http";
import router from "@/router";
import { createStore } from 'vuex'

export default createStore({
    state: {
        token: "",
        expiration: Date.now(),
        isBusy: false,
        error: "",
        name: "",
        roles: []
    },
    mutations: {

        setBusy: (state) => state.isBusy = true,
        clearBusy: (state) => state.isBusy = false,
        setError: (state, error) => state.error = error,
        clearError: (state) => state.error = "",
        setToken: (state, model) => {
            state.token = model.token;
            state.expiration = new Date(model.expiration)
            state.name = model.name;
            state.roles = model.roles;
        },

        clearToken: (state) => {
            state.token = "";
            state.expiration = Date.now();
        }
    },
    getters: {
        isAuthenticated: (state) => state.token.length > 0 && state.expiration > Date.now(),
        isLibrarian: (state) => state.roles.includes("Librarian"),
        userDetails: (state) => state.name
    },
    actions: {
        login: async ({ commit }, model) => {
            try {
                commit("setBusy");
                commit("clearError");
                const http = createHttp(false); // unsecured
                const result = await http.post("/api/auth", model);
                if (result.data.success) {
                    commit("setToken", result.data);
                    router.push("/");
                }
                else {
                    commit("setError", "Authentication Failed");
                }
            } catch {
                commit("setError", "Failed to login");
            } finally {
                commit("clearBusy");
            }
        },
        logout: ({ commit }) => {
            commit("clearToken");
            router.push("/");
        }
    }
})
