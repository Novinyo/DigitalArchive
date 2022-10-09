import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getCategories = createAsyncThunk(
  'setupApp/categories/getCategories',
  async (params, { getState }) => {
    const response = await axios.get('/api/DocumentTypes/categories');

    const data = await response.data;
    return data;
  }
);

const categoriesAdapter = createEntityAdapter({});

export const { selectAll: selectCategories, selectById: selectCategoriesById } =
  categoriesAdapter.getSelectors((state) => state.setupApp.categories);

const categoriesSlice = createSlice({
  name: 'setupApp/categories',
  initialState: categoriesAdapter.getInitialState([]),
  reducers: {},
  extraReducers: {
    [getCategories.fulfilled]: categoriesAdapter.setAll,
  },
});

export default categoriesSlice.reducer;
