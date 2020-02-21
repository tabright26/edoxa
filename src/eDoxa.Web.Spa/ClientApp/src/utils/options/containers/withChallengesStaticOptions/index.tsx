import React, { FunctionComponent, useEffect } from "react";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import { compose } from "recompose";
import { RootState } from "store/types";
import { loadChallengesStaticOptions } from "store/actions/static";
import { ChallengesStaticOptions } from "types";

export const withChallengesStaticOptions = (
  WrappedComponent: FunctionComponent
) => {
  type OwnProps = {};

  type StateProps = {
    options?: ChallengesStaticOptions;
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
      options: state.static.challenges
    };
  };

  const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
    dispatch: any
  ) => {
    return {
      loadOptions: () => {
        dispatch(loadChallengesStaticOptions());
      }
    };
  };

  const enhance = compose(connect(mapStateToProps, mapDispatchToProps));

  return enhance(EnhancedComponent);
};
