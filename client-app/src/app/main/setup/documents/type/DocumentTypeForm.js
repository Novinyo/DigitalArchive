import TextField from '@mui/material/TextField';
import { Controller, useFormContext } from 'react-hook-form';
import InputLabel from '@mui/material/InputLabel';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import FuseLoading from '@fuse/core/FuseLoading';
import { getCategories } from '../../store/categoriesSlice';

function DocumentTypeForm(props) {
  const dispatch = useDispatch();
  const methods = useFormContext();
  const { control, formState } = methods;
  const { errors } = formState;
  const { handleDuplicate } = props;
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    dispatch(getCategories()).then((action) => {
      if (action.payload) {
        const values = action.payload.map((item) => {
          return { id: item, name: item };
        });
        values.unshift({ id: '', name: 'Please select...' });
        setCategories(values);
        setLoading(false);
      }
    });
  }, [dispatch]);

  if (loading || categories.length === 0) {
    return (
      <div className="flex items-center justify-center h-full">
        <FuseLoading />
      </div>
    );
  }

  return (
    <div>
      <Controller
        name="code"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblCode">Code *</InputLabel>
            <TextField
              {...field}
              className="mt-8 mb-16"
              error={!!errors.code}
              required
              helperText={errors?.code?.message}
              labelid="lblCode"
              autoFocus
              id="code"
              variant="outlined"
              onBlur={() => {
                handleDuplicate('code', field.value);
              }}
              fullWidth
            />
          </>
        )}
      />
      <Controller
        name="name"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblName">Name *</InputLabel>
            <TextField
              {...field}
              className="mt-8 mb-16"
              error={!!errors.name}
              required
              helperText={errors?.name?.message}
              labelid="lblName"
              id="name"
              onBlur={() => {
                handleDuplicate('name', field.value);
              }}
              variant="outlined"
              fullWidth
            />
          </>
        )}
      />
      <Controller
        name="categoryId"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblSchoolType">Category *</InputLabel>
            <TextField
              select
              {...(field ?? '')}
              labelid="lblSchoolType"
              id="categoryId"
              error={!!errors.categoryId}
              helperText={errors?.categoryId?.message}
              className="mt-8 mb-16"
              fullWidth
            >
              {categories.map((item) => (
                <MenuItem key={item.id} value={item.name}>
                  {item.name}
                </MenuItem>
              ))}
            </TextField>
          </>
        )}
      />
      <Controller
        name="description"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblDescription">Description </InputLabel>
            <TextField
              {...field}
              className="mt-8 mb-16"
              id="description"
              labelid="lblDescription"
              type="text"
              multiline
              rows={5}
              variant="outlined"
              fullWidth
            />
          </>
        )}
      />

      <Controller
        name="active"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblActive">Active </InputLabel>
            <Checkbox
              {...field}
              checked={field.value}
              labelid="lblActive"
              id="active"
              size="small"
            />
          </>
        )}
      />
    </div>
  );
}

export default DocumentTypeForm;
