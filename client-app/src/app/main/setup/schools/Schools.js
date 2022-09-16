import useThemeMediaQuery from '@fuse/hooks/useThemeMediaQuery';
import FusePageCarded from '@fuse/core/FusePageCarded';
import withReducer from 'app/store/withReducer';
import SchoolsHeader from './SchoolHeader';
import SchoolsTable from './SchoolsTable';
import reducer from '../store';

function SchoolsPage() {
  const isMobile = useThemeMediaQuery((theme) => theme.breakpoints.down('lg'));
  return (
    <FusePageCarded
      header={<SchoolsHeader title="schools" newLink="new" />}
      content={<SchoolsTable />}
      scroll={isMobile ? 'normal' : 'content'}
    />
  );
}

export default withReducer('setupApp', reducer)(SchoolsPage);
