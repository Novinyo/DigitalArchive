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
import { selectCategories } from '../../store/categoriesSlice';

import {
  getDocumentType,
  newDocumentType,
  resetDocumentType,
  selectDocumentType,
} from '../../store/documentTypeSlice';
import reducer from '../../store';
import DocumentTypeHeader from './DocumentTypeHeader';
import DocumentTypeForm from './DocumentTypeForm';
import CategoriesService from '../../services/documentService/categoriesService';
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
  categoryId: yup.string().required('You must choose a category').max(1, 'Select a valid category'),
  isActive: yup.boolean(),
});

function DocumentType(props) {
  const documentType = useSelector(selectDocumentType);
  const { categories } = useSelector(selectCategories);
  const dispatch = useDispatch();
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
    function updateDocumentTypeState() {
      const { documentTypeId } = routeParams;
      console.log(documentTypeId);
      if (documentTypeId === 'new') {
        /**
         * Create New School type data
         */
        dispatch(newDocumentType());
      } else {
        /**
         * Get School type data
         */
        dispatch(getDocumentType(documentTypeId)).then((action) => {
          /**
           * If the requested school type is not exist show message
           */
          if (!action.payload) {
            setNoType(true);
          }
        });
      }
    }

    updateDocumentTypeState();
  }, [dispatch, routeParams]);

  const handleDuplicate = (type, value) => {
    if (value.trim().length > 2) {
      CategoriesService.checkIfExists(documentType.id, type, value)
        .then((res) => {
          if (res.data) {
            schema.canSave = false;
            setError(type, {
              type: 'manual',
              message: `Category type ${type} already exists`,
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
    if (!documentType) {
      return;
    }
    /**
     * Reset the form on school type state changes
     */
    reset(documentType);
  }, [documentType, reset]);

  useEffect(() => {
    return () => {
      /**
       * Reset School type on component unload
       */
      dispatch(resetDocumentType());
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
          There is no such document type!
        </Typography>
        <Button
          className="mt-24"
          component={Link}
          variant="outlined"
          to="/setup/documenttypes"
          color="inherit"
        >
          Go to School types Page
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
            dispatch(newDocumentType());
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
    (documentType &&
      routeParams.documentTypeId !== documentType.id &&
      routeParams.documentTypeId !== 'new')
  ) {
    return <FuseLoading />;
  }
  return (
    <FormProvider {...methods}>
      <FusePageCarded
        header={<DocumentTypeHeader returnURL="setup/documenttypes" title="Document type" />}
        content={
          <div className="p-16 sm:p-24 max-w-3xl">
            <DocumentTypeForm handleDuplicate={handleDuplicate} />
          </div>
        }
        scroll={isMobile ? 'normal' : 'content'}
      />
    </FormProvider>
  );
}

export default withReducer('setupApp', reducer)(DocumentType);
