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
import {
  getStaffType,
  newStaffType,
  resetStaffType,
  selectStaffType,
} from '../../store/staffTypeSlice';
import reducer from '../../store';
import TypeHeader from './TypeHeader';
import setupService from '../../services/schoolService/setupService';
import StaffTypeForm from './StaffTypeForm';

/**
 * Form Validation Schema
 */
const schema = yup.object().shape({
  code: yup
    .string()
    .required('You must enter a staff type code')
    .min(4, 'The staff type code must be at least 4 characters'),
  name: yup
    .string()
    .required('You must enter a staff type name')
    .min(4, 'The staff type name must be at least 4 characters'),
  description: yup
    .string()
    .required('You must provide a school description')
    .min(5, 'The description must be at least 5 characters'),
  // schoolId: yup.string().required('You must choose a school').min(10, 'Select a valid school'),
});

function StaffType(props) {
  const dispatch = useDispatch();
  const staffType = useSelector(selectStaffType);
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
    function updateStaffTypeState() {
      const { staffTypeId } = routeParams;
      if (staffTypeId === 'new') {
        /**
         * Create New Staff type data
         */
        dispatch(newStaffType());
      } else {
        /**
         * Get Staff type data
         */
        dispatch(getStaffType(staffTypeId)).then((action) => {
          /**
           * If the requested staff type is not exist show message
           */
          if (!action.payload) {
            setNoType(true);
          }
        });
      }
    }

    updateStaffTypeState();
  }, [dispatch, routeParams]);

  const handleDuplicate = (type, value) => {
    console.log(staffType);
    if (value.trim().length > 2) {
      setupService
        .checkStaffType(staffType.id, type, value)
        .then((res) => {
          if (res.data) {
            setError(type, {
              type: 'manual',
              message: `Staff type ${type} already exists`,
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
    if (!staffType) {
      return;
    }
    /**
     * Reset the form on staff type state changes
     */
    reset(staffType);
  }, [staffType, reset]);

  useEffect(() => {
    return () => {
      /**
       * Reset staff type on component unload
       */
      dispatch(resetStaffType());
      setNoType(false);
    };
  }, [dispatch]);

  /**
   * Show Message if the requested staff type does not exists
   */
  if (noType) {
    return (
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1, transition: { delay: 0.1 } }}
        className="flex flex-col flex-1 items-center justify-center h-full"
      >
        <Typography color="text.secondary" variant="h5">
          There is no such staff type!
        </Typography>
        <Button
          className="mt-24"
          component={Link}
          variant="outlined"
          to="/setup/stafftypes"
          color="inherit"
        >
          Go to Staff types Page
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
            dispatch(newStaffType());
          }}
        >
          Refresh
        </Button>
      </div>
    );
  }
  /**
   * Wait while product data is loading and form is setted
   */
  if (
    _.isEmpty(form) ||
    (staffType && routeParams.staffTypeId !== staffType.id && routeParams.staffTypeId !== 'new')
  ) {
    return <FuseLoading />;
  }

  return (
    <FormProvider {...methods}>
      <FusePageCarded
        header={<TypeHeader returnURL="setup/stafftypes" title="Staff type" />}
        content={
          <div className="p-16 sm:p-24 max-w-3xl">
            <StaffTypeForm handleDuplicate={handleDuplicate} />
          </div>
        }
        scroll={isMobile ? 'normal' : 'content'}
      />
    </FormProvider>
  );
}

export default withReducer('setupApp', reducer)(StaffType);
