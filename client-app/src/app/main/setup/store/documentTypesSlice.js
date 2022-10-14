import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getDocumentTypes = createAsyncThunk('setupApp/getDocumentTypes', async (active) => {
  try {
    const response = active
      ? await axios.get('/api/DocumentTypes/activedocumentTypes')
      : await axios.get('/api/DocumentTypes/documentTypes');
    return response.data;
  } catch (error) {
    console.log(error);
    return {
      error: {
        status: error.response.status,
        message: error.response.data.errors,
      },
    };
  }
});

export const removeDocumentTypes = createAsyncThunk(
  'setupApp/documentTypes',
  async (documentTypeIds, { dispatch, getState }) => {
    await axios.delete('/api/DocumentTypes', { data: documentTypeIds });

    return documentTypeIds;
  }
);

const documentTypesAdapter = createEntityAdapter({});

export const { selectAll: selectDocumentTypes, selectById: selectDocumentTypeById } =
  documentTypesAdapter.getSelectors((state) => state.setupApp.documentTypes);

const documentTypesSlice = createSlice({
  name: 'setupApp/documentTypes',
  initialState: documentTypesAdapter.getInitialState({
    searchText: '',
  }),
  reducers: {
    setDocumentTypesSearchText: {
      reducer: (state, action) => {
        state.searchText = action.payload;
      },
      prepare: (event) => ({ payload: event.target.value || '' }),
    },
  },
  extraReducers: {
    [getDocumentTypes.fulfilled]: documentTypesAdapter.setAll,
    [getDocumentTypes.rejected]: (state, action) => action.payload,
  },
});

export const { setDocumentTypesSearchText } = documentTypesSlice.actions;

export const selectDocumentTypesSearchText = ({ setupApp }) => setupApp.documentTypes.searchText;

export default documentTypesSlice.reducer;
