import React, { Component } from "react";
import { connect } from "react-redux";
import { loadGames } from "../actions/identity/identity";

const withUserGameHoc = WrappedComponent => {
  class UserGameContainer extends Component {
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
  )(UserGameContainer);
};

export default withUserGameHoc;
