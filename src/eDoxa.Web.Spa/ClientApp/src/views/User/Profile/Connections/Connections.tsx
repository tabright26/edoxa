import React, { Fragment } from "react";

import UserGameList from "components/User/Game/List";
import GenerateGameAuthFactorModal from "components/User/Game/AuthFactor/Modal/Generate/Generate";

const ProfileConnections = () => {
  return (
    <Fragment>
      <h5>GAME CONNECTIONS</h5>
      <UserGameList />
      <GenerateGameAuthFactorModal />
    </Fragment>
  );
};

export default ProfileConnections;
