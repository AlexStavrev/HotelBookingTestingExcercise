using System;
using System.Collections.Generic;
using HotelBooking.Controllers;
using HotelBooking.Models;
using HotelBooking.UnitTests.Fakes;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class RoomsControllerTests
    {
        private RoomsController controller;
        private Mock<IRepository<Room>> fakeRoomRepository;

        public RoomsControllerTests()
        {
            var rooms = new List<Room>
            {
                new Room { Id=1, Description="A" },
                new Room { Id=2, Description="B" },
            };

            // Create fake RoomRepository
            fakeRoomRepository = new Mock<IRepository<Room>>();

            // Implement fake GetAll() method.
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);


            // Implement fake Get() method.
            fakeRoomRepository.Setup(x => x.Get(2)).Returns(rooms[1]);


            // Alternative setup with argument matchers:

            // Any integer:
            //roomRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(rooms[1]);

            // Integers from 1 to 2
            //roomRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(rooms[1]);
            //roomRepository.Setup(x => x.Get(It.IsInRange<int>(1, 2, Range.Inclusive))).Returns(rooms[1]);

            // Create RoomsController
            controller = new RoomsController(fakeRoomRepository.Object);
        }

        [Fact]
        public void Index_ReturnsViewResultWithCorrectListOfRooms(){
            // Act
            var result = controller.Index() as ViewResult;
            var roomsList = result.Model as IList<Room>;
            var noOfRooms = roomsList.Count;

            // Assert
            Assert.Equal(2, noOfRooms);
        }

        [Fact]
        public void Details_RoomExists_ReturnsViewResultWithRoom(){
            // Act
            var result = controller.Details(2) as ViewResult;
            var room = result.Model as Room;
            var roomId = room.Id;

            // Assert
            Assert.InRange<int>(roomId, 1, 2);
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLargerThanZero_RemoveIsCalled()
        {
            // Act
            controller.DeleteConfirmed(1);

            // Assert against the mock object
            fakeRoomRepository.Verify(x => x.Remove(It.IsAny<int>()));
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLessThanOne_RemoveIsNotCalled()
        {
            // Act
            controller.DeleteConfirmed(0);

            // Assert against the mock object
            fakeRoomRepository.Verify(x => x.Remove(It.IsAny<int>()), Times.Never());        
        }

    }
}
