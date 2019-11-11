import React, { FunctionComponent } from "react";
import { CardDeck } from "reactstrap";
import Loading from "components/Shared/Loading";
import { withGames } from "store/root/games/container";
import { GamesState } from "store/root/games/types";
import { compose } from "recompose";
import GameItem from "components/User/Game/List/Item";

interface Props {
  games: GamesState;
}

const UserGameList: FunctionComponent<Props> = ({ games: { data, error, loading } }) => {
  return loading ? (
    <Loading />
  ) : (
    <CardDeck className="my-4">
      {Object.entries(data).map((game, index) => (
        <GameItem key={index} option={game[1]} />
      ))}
    </CardDeck>
  );
};

const enhance = compose<any, any>(withGames);

export default enhance(UserGameList);
