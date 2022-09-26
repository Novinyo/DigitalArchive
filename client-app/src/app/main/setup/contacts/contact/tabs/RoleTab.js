import { Controller, useFormContext } from 'react-hook-form';

function RolesTab(props) {
  const methods = useFormContext();
  const { control, formState } = methods;
  return (
    <div>
      <Controller control={control} name="roles" />
    </div>
  );
}

export default RolesTab;
