import FuseLoading from '@fuse/core/FuseLoading';
import TextField from '@mui/material/TextField';
import { Controller, useFormContext } from 'react-hook-form';
import InputLabel from '@mui/material/InputLabel';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import { useState, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { getSchoolTypes } from '../store/schoolTypesSlice';

function SchoolForm(props) {
  const dispatch = useDispatch();
  const methods = useFormContext();
  const { control, formState } = methods;
  const { errors } = formState;
  const [loading, setLoading] = useState(true);
  const [schoolTypes, setSchoolTypes] = useState([]);

  useEffect(() => {
    dispatch(getSchoolTypes(true)).then((action) => {
      if (action.payload) {
        const values = action.payload.map((item) => {
          return { id: item.id, name: item.name };
        });
        values.unshift({ id: '-1', name: 'Please select...' });
        setSchoolTypes(values);
        setLoading(false);
      }
    });
  }, [dispatch]);

  if (loading || schoolTypes.length < 1) {
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
              id="code"
              variant="outlined"
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
              type="text"
              helperText={errors?.name?.message}
              labelid="lblName"
              id="name"
              variant="outlined"
              fullWidth
            />
          </>
        )}
      />
      <Controller
        name="schoolTypeId"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblSchoolType">School Type *</InputLabel>
            <TextField
              select
              {...(field ?? '-1')}
              labelid="lblSchoolType"
              id="schooltypeId"
              error={!!errors.schoolTypeId}
              helperText={errors?.schoolTypeId?.message}
              className="mt-8 mb-16"
              fullWidth
            >
              {schoolTypes.map((item) => (
                <MenuItem key={item.id} value={item.id}>
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
              required
              error={!!errors.description}
              helperText={errors?.description?.message}
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

export default SchoolForm;
