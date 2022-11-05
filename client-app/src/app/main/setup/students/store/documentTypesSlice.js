import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getDocumentTypes = createAsyncThunk(
  'studentsApp/documents/getDocumentTypes',
  async (params, { getState }) => {
    const response = await axios.get('/api/DocumentTypes/activeDocumentTypes');

    const data = await response.data;
    const types = data.map((x) => {
      return { id: x.id, name: x.name };
    });
    types.unshift({ id: '', name: 'Please select...' });

    return types;
  }
);

const documentTypesAdapter = createEntityAdapter({});

export const { selectAll: selectDocumentTypes, selectById: selectDocumentTypesById } =
  documentTypesAdapter.getSelectors((state) => state.studentsApp.documentTypes);

const documentTypesSlice = createSlice({
  name: 'studentsApp/documentTypes',
  initialState: documentTypesAdapter.getInitialState([]),
  reducers: {},
  extraReducers: {
    [getDocumentTypes.fulfilled]: documentTypesAdapter.setAll,
  },
});

export default documentTypesSlice.reducer;
