import React, { Component } from "react";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";

export default class Room extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isAlive: 'isAlive',
      hint: 'hint',
      answer: 'answer',
      isHost: false,
    };
    this.roomCode = this.props.match.params.roomCode;
    this.getRoomDetails();
  }

  getRoomDetails() {    // contains the information of Room Page
    fetch("/api/get-room" + "?code=" + this.roomCode)
      .then((response) => response.json())
      .then((data) => {
        this.setState({
          isAlive: data.isAlive,
          hint: data.hint,
          answer: data.answer,
          isHost: data.is_host,
        });
      });
  }

  render() {
    return (
      <Grid container spacing={1}>
        <Grid item xs={8} align="center">
          <br></br><br></br><br></br>
          <Typography component="h2" variant="h2">
          Code:   {this.roomCode}
          </Typography>
        </Grid>
        <Grid item xs={8} align="center">
          <Typography component="h3" variant="h3">
          Answer:{this.state.answer}
          </Typography>
        </Grid>
        <Grid item xs={8} align="center">
          <Typography component="h5" variant="h5">
          Hint: {this.state.hint}
          </Typography>
        </Grid>
        <Grid item xs={8} align="center">
          <Typography component="h5" variant="h5">
          Is Alive? {this.state.isAlive.toString()} 
          </Typography>
        </Grid>
        <Grid item xs={8} align="center">
          <Typography component="h5" variant="h5">
          Is Host: {this.state.isHost.toString()}
          </Typography>
        </Grid>
      </Grid>
    );
  }
}
