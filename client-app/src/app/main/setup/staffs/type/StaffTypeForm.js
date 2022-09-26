import FuseLoading from '@fuse/core/FuseLoading';
import TextField from '@mui/material/TextField';
import { Controller, useFormContext } from 'react-hook-form';
import InputLabel from '@mui/material/InputLabel';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import { useState, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { getSchools } from '../../store/schoolsSlice';

function StaffTypeForm(props) {
  const dispatch = useDispatch();
  const methods = useFormContext();
  const { control, formState } = methods;
  const { errors } = formState;
  const [loading, setLoading] = useState(true);
  const [schools, setSchools] = useState([]);
  const { handleDuplicate } = props;

  useEffect(() => {
    dispatch(getSchools(true)).then((action) => {
      if (action.payload) {
        const values = action.payload.map((item) => {
          return { id: item.id, name: item.name };
        });
        values.unshift({ id: '-1', name: 'Please select...' });
        setSchools(values);
        setLoading(false);
      }
    });
  }, [dispatch]);

  if (loading || schools.length < 1) {
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
              type="text"
              helperText={errors?.name?.message}
              labelid="lblName"
              id="name"
              variant="outlined"
              onBlur={() => {
                handleDuplicate('name', field.value);
              }}
              fullWidth
            />
          </>
        )}
      />
      {/* <Controller
        name="schoolId"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblSchool">School *</InputLabel>
            <TextField
              select
              {...(field ?? '-1')}
              labelid="lblSchool"
              id="schoolId"
              error={!!errors.schoolId}
              helperText={errors?.schoolId?.message}
              className="mt-8 mb-16"
              fullWidth
            >
              {schools.map((item) => (
                <MenuItem key={item.id} value={item.id}>
                  {item.name}
                </MenuItem>
              ))}
            </TextField>
          </>
        )}
      /> */}
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

export default StaffTypeForm;
