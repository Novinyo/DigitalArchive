import { lazy } from 'react';
import StudentForm from './student/StudentForm';
import StudentView from './student/StudentView';

const StudentsApp = lazy(() => import('./StudentsApp'));

const StudentsAppConfig = {
  settings: {
    layout: {
      config: {},
    },
  },
  routes: [
    {
      path: 'setup/students',
      element: <StudentsApp />,
      children: [
        {
          path: ':id',
          element: <StudentView />,
        },
        {
          path: ':id/edit',
          element: <StudentForm />,
        },
      ],
    },
  ],
};

export default StudentsAppConfig;
