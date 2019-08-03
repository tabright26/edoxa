import React from 'react';
import { Card } from 'react-bootstrap';

import logo from '../../assets/images/logos/LeagueOfLegends.png';

const ChallengeLogo = () => (
  <Card className="bg-dark mx-3 my-4">
    <Card.Img className="p-4" src={logo} />
  </Card>
);

export default ChallengeLogo;
