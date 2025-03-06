import React from 'react';
import { Typography, Box, Button } from '@mui/material';
import { Link } from 'react-router-dom';

const NotFoundPage: React.FC = () => {
  return (
    <Box sx={{ textAlign: 'center', mt: 5 }}>
      <Typography variant="h1">404</Typography>
      <Typography variant="h4" gutterBottom>Page Not Found</Typography>
      <Typography variant="body1" sx={{ mb: 3 }}>
        The page you are looking for doesn't exist or has been moved.
      </Typography>
      <Button variant="contained" component={Link} to="/">
        Go to Home
      </Button>
    </Box>
  );
};

export default NotFoundPage;