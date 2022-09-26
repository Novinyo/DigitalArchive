import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getTags = createAsyncThunk(
  'contactsApp/tags/getTags',
  async (params, { getState }) => {
    const response = await axios.get('/api/StaffType/staffTypes');

    const data = await response.data;
    const types = data.map((x) => {
      return { id: x.id, name: x.name };
    });
    types.unshift({ id: '', name: 'Please select...' });

    return types;
  }
);

const tagsAdapter = createEntityAdapter({});

export const { selectAll: selectTags, selectById: selectTagsById } = tagsAdapter.getSelectors(
  (state) => state.contactsApp.tags
);

const tagsSlice = createSlice({
  name: 'contactsApp/tags',
  initialState: tagsAdapter.getInitialState([]),
  reducers: {},
  extraReducers: {
    [getTags.fulfilled]: tagsAdapter.setAll,
  },
});

export default tagsSlice.reducer;
