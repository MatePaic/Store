import { configureStore } from "@reduxjs/toolkit";
import { counterSlice } from "../../features/contact/counterReducer";
import { useDispatch, useSelector } from "react-redux";
import { catalogApi } from "../../features/catalog/catalogApi";
import { uiSlice } from "../layout/uiSlice";
import { errorApi } from "../../features/about/errorApi";
import { shoppingCartApi } from "../../features/shoppingCart/shoppingCartApi";

// export function configureTheStore() {
//     return legacy_createStore(counterReducer);
// }

export const store = configureStore({
    reducer: {
        [catalogApi.reducerPath]: catalogApi.reducer,
        [shoppingCartApi.reducerPath]: shoppingCartApi.reducer,
        [errorApi.reducerPath]: errorApi.reducer,
        counter: counterSlice.reducer,
        ui: uiSlice.reducer
    },
    middleware: (getDefaultMiddlewere) => 
        getDefaultMiddlewere().concat(
            catalogApi.middleware,
            errorApi.middleware,
            shoppingCartApi.middleware
        )
});

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export const useAppDispatch = useDispatch.withTypes<AppDispatch>()
export const useAppSelector = useSelector.withTypes<RootState>()

