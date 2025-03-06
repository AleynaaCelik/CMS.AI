import React from 'react';
import { Typography, Box, TextField, Button } from '@mui/material';

const SearchPage: React.FC = () => {
  return (
    <Box>
      <Typography variant="h4" gutterBottom>Search</Typography>
      <Box sx={{ display: 'flex', gap: 1, mb: 4 }}>
        <TextField fullWidth label="Search content" />
        <Button variant="contained">Search</Button>
      </Box>
      <Typography variant="body1">
        Enter your search query above to find content.
      </Typography>
    </Box>
  );
};

export default SearchPage;