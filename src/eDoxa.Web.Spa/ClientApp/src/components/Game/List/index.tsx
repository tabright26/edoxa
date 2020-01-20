import React, { FunctionComponent } from "react";
import { CardDeck } from "reactstrap";
import { compose } from "recompose";
import GameListItem from "components/Game/List/Item";
import GameCredentialModal from "components/Game/Credential/Modal";
import { GameOptions } from "types";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";

type OwnProps = {};

type StateProps = { games: GameOptions[] };

type InnerProps = StateProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({ games }) => (
  <>
    <CardDeck className="my-4">
      {games.map((gameOptions, index) => (
        <GameListItem key={index} gameOptions={gameOptions} />
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
    games: state.static.games.games
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(List);
