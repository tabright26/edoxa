import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadGameCredential } from "store/root/user/games/actions";
import { RootState } from "store/types";

export const withUserGames = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserGames();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      games: state.root.user.games
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: any) => {
    return {
      loadUserGames: () => dispatch(loadGameCredential(ownProps.game))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
