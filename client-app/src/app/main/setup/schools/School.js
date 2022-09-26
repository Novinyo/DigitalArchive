import FusePageCarded from '@fuse/core/FusePageCarded';
import { useDeepCompareEffect } from '@fuse/hooks';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import withReducer from 'app/store/withReducer';
import { motion } from 'framer-motion';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useParams } from 'react-router-dom';
import { FormProvider, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import useThemeMediaQuery from '@fuse/hooks/useThemeMediaQuery';
import _ from '@lodash';
import clsx from 'clsx';
import FuseLoading from '@fuse/core/FuseLoading';
import { getSchool, newSchool, resetSchool, selectSchool } from '../store/schoolSlice';
import reducer from '../store';
import SchoolHeader from './SchoolHeader';
import SchoolForm from './SchoolForm';
import setupService from '../services/schoolService/setupService';
/**
 * Form Validation Schema
 */
const schema = yup.object().shape({
  code: yup
    .string()
    .required('You must enter a school code')
    .min(4, 'The school code must be at least 4 characters'),
  name: yup
    .string()
    .required('You must enter a school name')
    .min(4, 'The school name must be at least 4 characters'),
  description: yup
    .string()
    .required('You must provide a school description')
    .min(10, 'The description must be at least 10 characters'),
  schoolTypeId: yup
    .string()
    .required('You must choose a school type')
    .min(10, 'Select a valid school type'),
});

function SchoolPage(props) {
  const dispatch = useDispatch();
  const school = useSelector(selectSchool);
  const isMobile = useThemeMediaQuery((theme) => theme.breakpoints.down('lg'));
  const routeParams = useParams();
  const [noSchool, setNoSchool] = useState(false);

  const methods = useForm({
    mode: 'onChange',
    defaultValues: {},
    resolver: yupResolver(schema),
  });
  const { reset, watch, control, onChange, setError, formState } = methods;
  const { errors } = formState;
  const form = watch();

  useDeepCompareEffect(() => {
    function updateSchoolState() {
      const { schoolId } = routeParams;

      if (schoolId === 'new') {
        dispatch(newSchool());
      } else {
        dispatch(getSchool(schoolId)).then((action) => {
          if (!action.payload) {
            setNoSchool(true);
          }
        });
      }
    }

    updateSchoolState();
  }, [dispatch, routeParams]);

  const handleDuplicate = (type, value) => {
    if (value.trim().length > 2) {
      setupService
        .checkSchool(school.id, type, value)
        .then((res) => {
          if (res.data) {
            schema.canSave = false;
            setError(type, {
              type: 'manual',
              message: `School ${type} already exists`,
            });
          }
        })
        .catch((_errors) => {
          _errors.forEach((error) => {
            setError(type, {
              type: 'manual',
              message: error.message,
            });
          });
        });
    }
  };

  useEffect(() => {
    console.log(school);
    if (!school) {
      return;
    }

    reset(school);
  }, [school, reset]);

  useEffect(() => {
    return () => {
      dispatch(resetSchool());
      setNoSchool(false);
    };
  }, [dispatch]);

  /**
   * Show Message if the requested school type does not exists
   */
  if (noSchool) {
    return (
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1, transition: { delay: 0.1 } }}
        className="flex flex-col flex-1 items-center justify-center h-full"
      >
        <Typography color="text.secondary" variant="h5">
          There is no such school!
        </Typography>
        <Button
          className="mt-24"
          component={Link}
          variant="outlined"
          to="/setup/schools"
          color="inherit"
        >
          Go to Schools Page
        </Button>
      </motion.div>
    );
  }
  /**
   * Display error if app crash
   */
  if (form?.status >= 400) {
    const data = JSON.parse(form.config.data);

    return (
      <div className={clsx('flex flex-1 flex-col items-center justify-center p-24')}>
        <Typography sx={{ color: 'red' }}>{form?.data.title}</Typography>
        <Button
          className="whitespace-nowrap mx-4"
          variant="contained"
          sx={{ backgroundColor: 'red', color: 'white' }}
          onClick={() => {
            dispatch(newSchool());
          }}
        >
          Refresh
        </Button>
      </div>
    );
  }
  if (
    _.isEmpty(form) ||
    (school && routeParams.schoolId !== school.id && routeParams.schoolId !== 'new')
  ) {
    return <FuseLoading />;
  }
  return (
    <FormProvider {...methods}>
      <FusePageCarded
        header={<SchoolHeader />}
        content={
          <div className="p-16 sm:p-24 max-w-3xl">
            <div className="p-16 sm:p-24 max-w-3xl">
              <SchoolForm handleDuplicate={handleDuplicate} />
            </div>
          </div>
        }
        scroll={isMobile ? 'normal' : 'content'}
      />
    </FormProvider>
  );
}

export default withReducer('setupApp', reducer)(SchoolPage);
