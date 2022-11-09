import Button from '@mui/material/Button';
import Tab from '@mui/material/Tab';
import Tabs from '@mui/material/Tabs';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import FuseLoading from '@fuse/core/FuseLoading';
import _ from '@lodash';
import * as yup from 'yup';
import { FormProvider, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup/dist/yup';
import Box from '@mui/system/Box';

import {
  addStudent,
  getStudent,
  newStudent,
  removeStudent,
  selectStudent,
  updateStudent,
} from '../store/studentSlice';
import StudentInfo from './tabs/StudentInfo';
import ParentInfo from './tabs/ParentInfo';
import AddressInfo from './tabs/AddressInfo';
import DocumentsTab from './tabs/DocumentsTab';

/**
 * Form Validation Schema
 */
const date = new Date();
const maxDate = new Date(date.getFullYear() - 4, date.getMonth(), date.getDate());

const schema = yup.object().shape({
  code: yup.string().required('You must enter a student code'),
  firstName: yup.string().required("Student's first name is required"),
  middleName: yup.string(),
  lastName: yup.string().required("Student's last name is required"),
  faFName: yup.string().required("Father's first name is required"),
  faLName: yup.string().required("Father's last name is required"),
  moFName: yup.string().required("Mother's first name is required"),
  moLName: yup.string().required("Mother's last name is required"),
  faEmail: yup.string().email('You must enter a valid email'),
  moEmail: yup.string().email('You must enter a valid email'),
  faPhone: yup.string().required("Father's phone number is required"),
  moPhone: yup.string().required("Mother's phone number is required"),
  birthdate: yup
    .date()
    .max(new Date(maxDate), 'Student must be 4 and above')
    .required('Date of birth is required'),
  dateJoined: yup
    .date()
    .max(new Date(), 'Student entrance date cannot be in the future')
    .required('You must select a hire date'),
  emergencyContact: yup.string().required('Kindly provide an emergency contact'),
  documents: yup.array().min(1),
});

const StudentForm = (props) => {
  const student = useSelector(selectStudent);
  const routeParams = useParams();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [tabValue, setTabValue] = useState(0);

  const methods = useForm({
    mode: 'onChange',
    defaultValues: {},
    resolver: yupResolver(schema),
  });
  const { control, watch, reset, handleSubmit, formState, getValues } = methods;
  const { isValid, dirtyFields, errors } = formState;
  const form = watch();

  useEffect(() => {
    if (routeParams.id === 'new') {
      dispatch(newStudent());
    } else {
      dispatch(getStudent(routeParams.id));
    }
  }, [dispatch, routeParams]);

  useEffect(() => {
    reset({ ...student });
  }, [student, reset]);

  /**
   * Form Submit
   */
  function onSubmit(data) {
    if (routeParams.id === 'new') {
      dispatch(addStudent(data)).then(({ payload }) => {
        navigate(`/setup/students/${payload.id}`);
      });
    } else {
      dispatch(updateStudent(data, routeParams.id)).then(({ payload }) => {
        navigate(`/setup/students/${payload.id}`);
      });
    }
  }

  function handleRemoveStudent() {
    dispatch(removeStudent(student.id)).then(() => {
      navigate(`/setup/students`);
    });
  }

  function handleTabChange(event, value) {
    setTabValue(value);
  }

  if (_.isEmpty(form) || !student) {
    return <FuseLoading />;
  }
  return (
    <FormProvider {...methods}>
      <Box
        className="relative w-full h-160 sm:h-192 px-32 sm:px-48"
        sx={{
          backgroundColor: 'background.default',
        }}
      >
        <img
          className="absolute inset-0 object-cover w-full h-full"
          src="assets/images/cards/14-640x480.jpg"
          alt="user background"
        />
      </Box>
      <div className="relative flex flex-col flex-auto px-24 sm:px-48">
        <Tabs
          value={tabValue}
          onChange={handleTabChange}
          indicatorColor="secondary"
          variant="scrollable"
          scrollButtons="auto"
          classes={{ root: 'w-full h-64 border-b-1' }}
        >
          <Tab className="h-64" label="Student Info" />
          <Tab className="h-64" label="Parent Info" />
          <Tab className="h-64" label="Health & Address" />
          <Tab className="h-64" label="Documents" />
        </Tabs>
        <div className="p-16 sm:p-24 max-w-3xl">
          <div className={tabValue !== 0 ? 'hidden' : ''}>
            <StudentInfo />
          </div>
          <div className={tabValue !== 1 ? 'hidden' : ''}>
            <ParentInfo />
          </div>
          <div className={tabValue !== 2 ? 'hidden' : ''}>
            <AddressInfo />
          </div>
          <div className={tabValue !== 3 ? 'hidden' : ''}>
            <DocumentsTab />
          </div>
        </div>
      </div>
      <Box
        className="flex items-center mt-40 py-14 pr-16 pl-4 sm:pr-48 sm:pl-36 border-t"
        sx={{ backgroundColor: 'background.default' }}
      >
        {routeParams.id !== 'new' && (
          <Button color="error" onClick={handleRemoveStudent}>
            Delete
          </Button>
        )}
        <Button className="ml-auto" component={NavLinkAdapter} to={-1}>
          Cancel
        </Button>
        <Button
          className="ml-8"
          variant="contained"
          color="secondary"
          disabled={_.isEmpty(dirtyFields) || !isValid}
          onClick={handleSubmit(onSubmit)}
        >
          Save
        </Button>
      </Box>
    </FormProvider>
  );
};

export default StudentForm;
