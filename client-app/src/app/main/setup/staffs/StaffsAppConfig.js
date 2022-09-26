import { lazy } from 'react';
import StaffForm from './staff/StaffForm';
import StaffView from './staff/StaffView';

const StaffsApp = lazy(() => import('./StaffsApp'));
const StaffType = lazy(() => import('./type/StaffType'));
const StaffTypes = lazy(() => import('./type/StaffTypes'));

const StaffsAppConfig = {
  settings: {
    layout: {
      config: {},
    },
  },
  routes: [
    {
      path: 'setup/stafftypes',
      element: <StaffTypes />,
    },
    {
      path: 'setup/stafftypes/:staffTypeId/*',
      element: <StaffType />,
    },
    {
      path: 'setup/staffs',
      element: <StaffsApp />,
      chilren: [
        {
          path: ':id',
          element: <StaffView />,
        },
        {
          path: ':id/edit',
          element: <StaffForm />,
        },
      ],
    },
  ],
};

export default StaffsAppConfig;
