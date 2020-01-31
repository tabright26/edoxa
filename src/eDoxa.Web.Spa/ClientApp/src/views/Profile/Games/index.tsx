import React, { FunctionComponent } from "react";

import GameList from "components/Game/List";

const ProfileGames: FunctionComponent = () => {
  return (
    <>
      <h5 className="text-uppercase my-4">GAME CONNECTIONS</h5>
      <GameList />
    </>
  );
};

export default ProfileGames;
