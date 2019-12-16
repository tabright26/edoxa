import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadGames } from "store/actions/game";
import { RootState } from "store/types";

export const withGames = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadGames();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      games: state.root.game
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadGames: () => dispatch(loadGames())
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
