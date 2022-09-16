import FuseLoading from '@fuse/core/FuseLoading';
import FusePageCarded from '@fuse/core/FusePageCarded';
import { useDeepCompareEffect } from '@fuse/hooks';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import withReducer from 'app/store/withReducer';
import { motion } from 'framer-motion';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useParams } from 'react-router-dom';
import _ from '@lodash';
import { FormProvider, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import clsx from 'clsx';
import useThemeMediaQuery from '@fuse/hooks/useThemeMediaQuery';

import {
  getSchoolType,
  newSchoolType,
  resetSchoolType,
  selectSchoolType,
} from '../../store/schoolTypeSlice';
import reducer from '../../store';
import TypeHeader from './TypeHeader';
import SchoolTypeForm from './SchoolTypeForm';
import setupService from '../../services/schoolService/setupService';
/**
 * Form Validation Schema
 */
const schema = yup.object().shape({
  code: yup
    .string()
    .required('You must enter a school type code')
    .min(3, 'The school type code must be at least 3 characters'),
  name: yup
    .string()
    .required('You must enter a school type name')
    .min(3, 'The school type name must be at least 3 characters'),
  isActive: yup.boolean(),
});

function SchoolType(props) {
  const dispatch = useDispatch();
  const schoolType = useSelector(selectSchoolType);
  const isMobile = useThemeMediaQuery((theme) => theme.breakpoints.down('lg'));
  const routeParams = useParams();
  const [noType, setNoType] = useState(false);

  const methods = useForm({
    mode: 'onChange',
    defaultValues: {},
    resolver: yupResolver(schema),
  });
  const { reset, watch, control, onChange, setError, formState } = methods;
  const { errors } = formState;
  const form = watch();

  useDeepCompareEffect(() => {
    function updateSchoolTypeState() {
      const { schoolTypeId } = routeParams;
      if (schoolTypeId === 'new') {
        /**
         * Create New School type data
         */
        dispatch(newSchoolType());
      } else {
        /**
         * Get School type data
         */
        dispatch(getSchoolType(schoolTypeId)).then((action) => {
          /**
           * If the requested school type is not exist show message
           */
          if (!action.payload) {
            setNoType(true);
          }
        });
      }
    }

    updateSchoolTypeState();
  }, [dispatch, routeParams]);

  const handleDuplicate = (type, value) => {
    if (value.trim().length > 2) {
      setupService
        .checkIfExists(schoolType.id, type, value)
        .then((res) => {
          if (res.data) {
            schema.canSave = false;
            setError(type, {
              type: 'manual',
              message: `School type ${type} already exists`,
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
    if (!schoolType) {
      return;
    }
    /**
     * Reset the form on school type state changes
     */
    reset(schoolType);
  }, [schoolType, reset]);

  useEffect(() => {});

  useEffect(() => {
    return () => {
      /**
       * Reset School type on component unload
       */
      dispatch(resetSchoolType());
      setNoType(false);
    };
  }, [dispatch]);
  /**
   * Show Message if the requested school type does not exists
   */
  if (noType) {
    return (
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1, transition: { delay: 0.1 } }}
        className="flex flex-col flex-1 items-center justify-center h-full"
      >
        <Typography color="text.secondary" variant="h5">
          There is no such school type!
        </Typography>
        <Button
          className="mt-24"
          component={Link}
          variant="outlined"
          to="/schooltypes"
          color="inherit"
        >
          Go to School types Page
        </Button>
      </motion.div>
    );
  }

  /**
   * Wait while product data is loading and form is setted
   */
  if (form?.status >= 400) {
    const data = JSON.parse(form.config.data);

    // schoolType = data;
    // form = data;
    return (
      <div className={clsx('flex flex-1 flex-col items-center justify-center p-24')}>
        <Typography sx={{ color: 'red' }}>{form?.data.title}</Typography>
        <Button
          className="whitespace-nowrap mx-4"
          variant="contained"
          sx={{ backgroundColor: 'red', color: 'white' }}
          onClick={() => {
            dispatch(newSchoolType());
          }}
        >
          Refresh
        </Button>
      </div>
    );
  }
  if (
    _.isEmpty(form) ||
    (schoolType && routeParams.schoolTypeId !== schoolType.id && routeParams.schoolTypeId !== 'new')
  ) {
    return <FuseLoading />;
  }
  return (
    <FormProvider {...methods}>
      <FusePageCarded
        header={<TypeHeader returnURL="schooltypes" title="School type" />}
        content={
          <div className="p-16 sm:p-24 max-w-3xl">
            <SchoolTypeForm handleDuplicate={handleDuplicate} />
          </div>
        }
        scroll={isMobile ? 'normal' : 'content'}
      />
    </FormProvider>
  );
}

export default withReducer('setupApp', reducer)(SchoolType);
