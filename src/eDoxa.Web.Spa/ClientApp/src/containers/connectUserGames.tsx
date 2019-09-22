import React, { Component } from "react";
import { connect } from "react-redux";
import { loadGames } from "actions/identity/actionCreators";

const connectUserGames = WrappedComponent => {
  class Container extends Component<any> {
    componentDidMount() {
      this.props.actions.loadGames();
    }

    render() {
      return <WrappedComponent games={this.props.games} />;
    }
  }

  const mapStateToProps = state => {
    return {
      games: state.user.games
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadGames: () => dispatch(loadGames())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserGames;
