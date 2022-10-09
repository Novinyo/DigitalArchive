import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getDocumentType = createAsyncThunk(
  'setupApp/getDocumentType',
  async (schoolTypeId) => {
    try {
      const response = await axios.get(`/api/DocumentTypes/${schoolTypeId}`);
      const data = await response.data;

      return data === undefined
        ? null
        : {
            id: data.id,
            code: data.code,
            name: data.name,
            description: data.description,
            categoryId: data.category,
            active: data.active,
          };
    } catch (error) {
      return error.response;
    }
  }
);

export const getCategories = createAsyncThunk(
  'setupApp/getCategories',
  async (params, { getState }) => {
    const response = await axios.get('/api/DocumentTypes/categories');

    const data = await response.data;
    return data;
  }
);

export const checkIfExists = createAsyncThunk('setupApp/checkTypeExists', async (col) => {
  try {
    const response = await axios.get(`/api/documenttypes/exists/${col.key}/${col.value}`);

    const data = await response.data;

    return data === undefined ? false : data;
  } catch (error) {
    return error.response;
  }
});

export const saveDocumentType = createAsyncThunk(
  'setupApp/saveDocumentType',
  async (doc, { dispatch, getState }) => {
    const { id } = getState().setupApp;

    const docData = {
      code: doc.code.trim().toUpperCase(),
      name: doc.name.trim().toUpperCase(),
      description: doc.description.trim(),
      category: doc.categoryId.trim().toUpperCase(),
      active: doc.active,
    };

    console.log(docData);

    try {
      const response =
        doc.id === null
          ? await axios.post(`/api/documenttypes`, docData)
          : await axios.put(`api/documenttypes/${doc.id}`, docData);

      if (response.data.isSuccess) {
        return response.data;
      }
      return response.data;
    } catch (error) {
      console.log(error.response);
      return error.response;
    }
  }
);

export const deleteDocumentType = createAsyncThunk(
  'setupApp/deleteDocumentType',
  async (val, { dispatch, getState }) => {
    const { id } = getState().setupApp.documentType;
    await axios.delete(`/api/documenttypes/${id}`);
    return id;
  }
);

const documentTypeSlice = createSlice({
  name: 'setupApp/documenttype',
  initialState: null,
  reducers: {
    resetDocumentType: () => null,
    newDocumentType: {
      reducer: (state, action) => action.payload,
      prepare: (event) => ({
        payload: {
          id: null,
          code: '',
          name: '',
          description: '',
          categoryId: '',
          active: true,
        },
      }),
    },
  },
  extraReducers: {
    [getDocumentType.fulfilled]: (state, action) => action.payload,
    [saveDocumentType.fulfilled]: (state, action) => action.payload,
    [checkIfExists.fulfilled]: (state, action) => action.payload,
    [getCategories.fulfilled]: (state, action) => action.payload,
    [deleteDocumentType.fulfilled]: (state, action) => null,
  },
});

export const { newDocumentType, resetDocumentType } = documentTypeSlice.actions;

export const selectDocumentType = ({ setupApp }) => setupApp.documentType;

export default documentTypeSlice.reducer;
