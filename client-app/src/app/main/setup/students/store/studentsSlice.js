import {
  createAsyncThunk,
  createEntityAdapter,
  createSelector,
  createSlice,
} from '@reduxjs/toolkit';
import axios from 'axios';
import FuseUtils from '@fuse/utils';
import { addStudent, removeStudent, updateStudent } from './studentSlice';

export const getStudents = createAsyncThunk(
  'setupApp/students/getStudents',
  async (params, { getState }) => {
    const response = await axios.get('/api/students/students');

    const data = await response.data;

    return { data };
  }
);

const studentsAdapter = createEntityAdapter({});

export const selectSearchText = ({ studentsApp }) => studentsApp.students.searchText;

export const { selectAll: selectStudents, selectById: selectStudentsById } =
  studentsAdapter.getSelectors((state) => state.studentsApp.students);

export const selectFilteredStudents = createSelector(
  [selectStudents, selectSearchText],
  (students, searchText) => {
    if (searchText.length === 0) {
      return students;
    }
    return FuseUtils.filterArrayByString(students, searchText);
  }
);

export const selectGroupedFilteredStudents = createSelector(
  [selectFilteredStudents],
  (students) => {
    return students
      .sort((a, b) => a.lastName.localeCompare(b.lastName, 'es', { sensitivity: 'base' }))
      .reduce((r, e) => {
        // get first letter of name of current element
        const group = e.lastName[0];
        // if there is no property in accumulator with this letter create it
        if (!r[group]) r[group] = { group, children: [e] };
        // if there is push current element to children array for that letter
        else r[group].children.push(e);
        // return accumulator
        return r;
      }, {});
  }
);

const studentsSlice = createSlice({
  name: 'setupApp/students',
  initialState: studentsAdapter.getInitialState({
    searchText: '',
  }),
  reducers: {
    setStudentsSearchText: {
      reducer: (state, action) => {
        state.searchText = action.payload;
      },
      prepare: (event) => ({ payload: event.target.value || '' }),
    },
  },
  extraReducers: {
    [updateStudent.fulfilled]: studentsAdapter.upsertOne,
    [addStudent.fulfilled]: studentsAdapter.addOne,
    [removeStudent.fulfilled]: (state, action) => studentsAdapter.removeOne(state, action.payload),
    [getStudents.fulfilled]: (state, action) => {
      const { data, routeParams } = action.payload;
      studentsAdapter.setAll(state, data);
      state.searchText = '';
    },
  },
});

export const { setStudentsSearchText } = studentsSlice.actions;

export default studentsSlice.reducer;
