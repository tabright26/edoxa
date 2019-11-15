import React, { FunctionComponent } from "react";
import { CardDeck } from "reactstrap";
import Loading from "components/Shared/Loading";
import { withGames } from "store/root/games/container";
import { GamesState } from "store/root/games/types";
import { compose } from "recompose";
import GameListItem from "components/Game/List/Item";

interface Props {
  games: GamesState;
}

const UserGameList: FunctionComponent<Props> = ({
  games: { data, error, loading }
}) => {
  return loading ? (
    <Loading />
  ) : (
    <CardDeck className="my-4">
      {Object.entries(data).map((game, index) => (
        <GameListItem key={index} gameOption={game[1]} />
      ))}
    </CardDeck>
  );
};

const enhance = compose<any, any>(withGames);

export default enhance(UserGameList);
