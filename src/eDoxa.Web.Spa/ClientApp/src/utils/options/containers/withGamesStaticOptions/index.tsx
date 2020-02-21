import React, { FunctionComponent, useEffect } from "react";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import { compose } from "recompose";
import { RootState } from "store/types";
import { loadGamesStaticOptions } from "store/actions/static";
import { GamesStaticOptions } from "types";

export const withGamesStaticOptions = (
  WrappedComponent: FunctionComponent
) => {
  type OwnProps = {};

  type StateProps = {
    options?: GamesStaticOptions;
  };

  type DispatchProps = {
    loadOptions: () => void;
  };

  type Props = StateProps & DispatchProps;

  const EnhancedComponent: FunctionComponent<Props> = ({
    loadOptions,
    options,
    ...props
  }) => {
    useEffect(() => {
      loadOptions();
    }, [loadOptions]);
    return <WrappedComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<
    StateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      options: state.static.games
    };
  };

  const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
    dispatch: any
  ) => {
    return {
      loadOptions: () => {
        dispatch(loadGamesStaticOptions());
      }
    };
  };

  const enhance = compose(connect(mapStateToProps, mapDispatchToProps));

  return enhance(EnhancedComponent);
};
