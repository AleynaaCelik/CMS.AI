import React from 'react';
import { Typography, Button, Box } from '@mui/material';
import { Link } from 'react-router-dom';

const ContentList: React.FC = () => {
  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" gutterBottom>Content</Typography>
        <Button 
          variant="contained" 
          component={Link} 
          to="/content/create"
        >
          Add New
        </Button>
      </Box>
      <Typography>Content list will be displayed here.</Typography>
    </Box>
  );
};

export default ContentList;