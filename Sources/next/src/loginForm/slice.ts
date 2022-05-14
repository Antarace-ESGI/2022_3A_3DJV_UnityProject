import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import storage from "redux-persist/lib/storage";
import { PersistConfig } from "redux-persist/es/types";

export interface AuthState {
	value: number
}

const initialState: AuthState = {
	value: null,
};

export const tokenSlice = createSlice({
	name: "token",
	initialState,
	reducers: {
		setToken: (state, action: PayloadAction<number>) => {
			state.value = action.payload;
		},
	},
});

export const authPersistConfig: PersistConfig<AuthState> = {
	key: "auth",
	storage: storage,
	version: 1,
};

// Action creators are generated for each case reducer function
export const { setToken } = tokenSlice.actions;

export default tokenSlice.reducer;