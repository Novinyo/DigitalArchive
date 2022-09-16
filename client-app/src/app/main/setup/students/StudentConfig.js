import { lazy } from 'react';

const StudentTypePage = lazy(() => import('./StudentTypePage'));
const StudentPage = lazy(() => import('./StudentPage'));

const StudentConfig = {
  settings: {
    layout: {
      config: {},
    },
  },
  routes: [
    {
      path: 'students',
      children: [
        {
          path: '',
          element: <StudentPage />,
        },
        {
          path: 'studenttype',
          element: <StudentTypePage />,
        },
      ],
    },
  ],
};

export default StudentConfig;
