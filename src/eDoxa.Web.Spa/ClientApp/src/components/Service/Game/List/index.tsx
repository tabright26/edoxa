import React, { FunctionComponent } from "react";
import { CardDeck } from "reactstrap";
import { compose } from "recompose";
import GameListItem from "components/Service/Game/List/Item";
import GameCredentialModal from "components/Service/Game/Credential/Modal";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { Game } from "types/games";

type OwnProps = {};

type StateProps = { games: Game[] };

type InnerProps = StateProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({ games }) => (
  <>
    <CardDeck className="my-4">
      {games.map((game, index) => (
        <GameListItem key={index} game={game} />
      ))}
    </CardDeck>
    <GameCredentialModal.Link />
    <GameCredentialModal.Unlink />
  </>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    games: state.static.games.games.map(x => x.name)
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(List);
