import React, { FunctionComponent } from "react";
import { CardDeck } from "reactstrap";
import Loading from "components/Shared/Loading";
import { withGames } from "store/root/game/container";
import { GamesState } from "store/root/game/types";
import { compose } from "recompose";
import GameListItem from "components/Game/List/Item";
import GameCredentialModal from "components/Game/Credential/Modal";

interface Props {
  games: GamesState;
}

const GameList: FunctionComponent<Props> = ({
  games: { data, error, loading }
}) => {
  return loading ? (
    <Loading />
  ) : (
    <>
      <CardDeck className="my-4">
        {Object.entries(data).map((game, index) => (
          <GameListItem key={index} gameOption={game[1]} />
        ))}
      </CardDeck>
      <GameCredentialModal.Link />
      <GameCredentialModal.Unlink />
    </>
  );
};

const enhance = compose<any, any>(withGames);

export default enhance(GameList);
