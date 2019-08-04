import React from 'react';
import { Spinner as BootstrapSpinner } from 'react-bootstrap';

const Spinner = () => (
  <BootstrapSpinner animation="border" role="status">
    <span className="sr-only">Loading...</span>
  </BootstrapSpinner>
);

export default Spinner;
