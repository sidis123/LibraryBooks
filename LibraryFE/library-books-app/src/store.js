// store.js
import { configureStore, createSlice } from "@reduxjs/toolkit";

// Create a slice of the state for authentication
const authSlice = createSlice({
  name: "auth",
  initialState: {
    isLoggedIn: false,
    user: null,
  },
  reducers: {
    login: (state, action) => {
      state.isLoggedIn = true;
      state.user = action.payload; // action.payload will hold user data
    },
    logout: (state) => {
      state.isLoggedIn = false;
      state.user = null;
    },
  },
});

// Export actions to use in components
export const { login, logout } = authSlice.actions;

// Create and configure the store
const store = configureStore({
  reducer: {
    auth: authSlice.reducer, // Add the auth reducer to the store
  },
});

export default store;
