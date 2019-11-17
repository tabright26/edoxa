import React, { Fragment } from "react";

import GameList from "components/Game/List";

const ProfileConnections = () => {
  return (
    <Fragment>
      <h5 className="text-uppercase my-4">GAME CONNECTIONS</h5>
      <GameList />
    </Fragment>
  );
};

export default ProfileConnections;
