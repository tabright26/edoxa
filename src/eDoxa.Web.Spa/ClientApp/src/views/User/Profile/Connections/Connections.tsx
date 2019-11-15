import React, { Fragment } from "react";

import GameList from "components/Game/List";
import GameAccountCredentialModal from "components/Game/Account/Credential/Modal";

const ProfileConnections = () => {
  return (
    <Fragment>
      <h5 className="text-uppercase my-4">GAME CONNECTIONS</h5>
      <GameList />
      <GameAccountCredentialModal.Link />
      <GameAccountCredentialModal.Unlink />
    </Fragment>
  );
};

export default ProfileConnections;
